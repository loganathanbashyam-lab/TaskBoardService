import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { UserService } from './services/user.service';
import { BoardService } from './services/board.service';
import { TaskService } from './services/task.service';
import { User } from './models/user';
import { Board } from './models/board';
import { TaskItem } from './models/task-item';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  apiError = signal<string | null>(null);
  users = signal<User[]>([]);
  selectedUser = signal<User | null>(null);
  boards = signal<Board[]>([]);
  selectedBoard = signal<Board | null>(null);
  tasks = signal<TaskItem[]>([]);
  editingTask = signal<TaskItem | null>(null);

  private readonly fb = inject(FormBuilder);

  userForm = this.fb.group({
    username: ['', Validators.required]
  });

  boardForm = this.fb.group({
    name: ['', Validators.required]
  });

  taskForm = this.fb.group({
    title: ['', Validators.required],
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
    assignedTo: [''],
    owner: [''],
    priority: ['Medium'],
    status: ['New'],
    boardId: this.fb.control<number | null>(null, Validators.required)
  });

  constructor(
    private userService: UserService,
    private boardService: BoardService,
    private taskService: TaskService
  ) {
    this.loadUsers();
  }

  private setError(message: string | null) {
    this.apiError.set(message);
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (users) => this.users.set(users),
      error: () => this.setError('Unable to load users from the backend.')
    });
  }

  selectUser(user: User): void {
    this.selectedUser.set(user);
    this.selectedBoard.set(null);
    this.tasks.set([]);
    this.boardForm.reset();
    this.taskForm.reset({ priority: 'Medium', status: 'New', boardId: null });
    this.loadBoards(user.id);
  }

  createUser(): void {
    if (this.userForm.invalid) return;

    const username = this.userForm.controls.username.value?.trim();
    if (!username) return;

    this.userService.createUser(username).subscribe({
      next: (user) => {
        this.users.update((list) => [...list, user]);
        this.userForm.reset();
        this.selectUser(user);
      },
      error: () => this.setError('Unable to create user.')
    });
  }

  deleteUser(userId: number): void {
    this.userService.deleteUser(userId).subscribe({
      next: () => {
        this.users.update((list) => list.filter((user) => user.id !== userId));
        if (this.selectedUser()?.id === userId) {
          this.selectedUser.set(null);
          this.boards.set([]);
          this.tasks.set([]);
        }
      },
      error: () => this.setError('Unable to delete user.')
    });
  }

  loadBoards(userId: number): void {
    this.boardService.getBoardsForUser(userId).subscribe({
      next: (boards) => this.boards.set(boards),
      error: () => this.setError('Unable to load boards for the selected user.')
    });
  }

  createBoard(): void {
    if (this.boardForm.invalid || !this.selectedUser()) return;

    const name = this.boardForm.controls.name.value?.trim();
    if (!name) return;

    const userId = this.selectedUser()!.id;
    this.boardService.createBoard(name, userId).subscribe({
      next: (board) => {
        this.boards.update((list) => [...list, board]);
        this.boardForm.reset();
      },
      error: () => this.setError('Unable to create board.')
    });
  }

  deleteBoard(boardId: number): void {
    this.boardService.deleteBoard(boardId).subscribe({
      next: () => {
        this.boards.update((list) => list.filter((board) => board.id !== boardId));
        if (this.selectedBoard()?.id === boardId) {
          this.selectedBoard.set(null);
          this.tasks.set([]);
        }
      },
      error: () => this.setError('Unable to delete board.')
    });
  }

  selectBoard(board: Board): void {
    this.selectedBoard.set(board);
    this.editingTask.set(null);
    this.taskForm.reset({
      title: '',
      startDate: '',
      endDate: '',
      assignedTo: '',
      owner: '',
      priority: 'Medium',
      status: 'New',
      boardId: board.id
    });
    this.loadTasks(board.id);
  }

  loadTasks(boardId: number): void {
    this.taskService.getTasksForBoard(boardId).subscribe({
      next: (tasks) => this.tasks.set(tasks),
      error: () => this.setError('Unable to load tasks for the selected board.')
    });
  }

  submitTask(): void {
    if (this.taskForm.invalid || !this.selectedBoard()) return;

    const startDate = this.taskForm.controls.startDate.value;
    const endDate = this.taskForm.controls.endDate.value;
    if (startDate && endDate && startDate > endDate) {
      this.setError('Start date cannot be after end date.');
      return;
    }

    const payload: TaskItem = {
      id: this.editingTask()?.id ?? 0,
      title: this.taskForm.controls.title.value?.trim() ?? '',
      startDate: startDate ?? '',
      endDate: endDate ?? '',
      assignedTo: this.taskForm.controls.assignedTo.value ?? '',
      owner: this.taskForm.controls.owner.value ?? '',
      priority: this.taskForm.controls.priority.value ?? 'Medium',
      status: this.taskForm.controls.status.value ?? 'New',
      boardId: this.selectedBoard()!.id
    };

    if (this.editingTask()) {
      this.taskService.updateTask(payload).subscribe({
        next: (updated) => {
          this.tasks.update((list) => list.map((task) => (task.id === updated.id ? updated : task)));
          this.clearTaskEdit();
        },
        error: () => this.setError('Unable to update task.')
      });
    } else {
      this.taskService.addTask(payload).subscribe({
        next: (task) => {
          this.tasks.update((list) => [...list, task]);
          this.clearTaskEdit();
        },
        error: (err) => {
          console.error('Task creation error:', err);
          this.setError('Unable to create task. Check console for details.');
        }
      });
    }
  }

  editTask(task: TaskItem): void {
    this.editingTask.set(task);
    this.taskForm.patchValue({
      title: task.title,
      startDate: task.startDate.slice(0, 10),
      endDate: task.endDate.slice(0, 10),
      assignedTo: task.assignedTo,
      owner: task.owner,
      priority: task.priority,
      status: task.status,
      boardId: task.boardId
    });
  }

  clearTaskEdit(): void {
    this.editingTask.set(null);
    this.taskForm.reset({
      title: '',
      startDate: '',
      endDate: '',
      assignedTo: '',
      owner: '',
      priority: 'Medium',
      status: 'New',
      boardId: this.selectedBoard()?.id ?? null
    });
    this.setError(null);
  }

  deleteTask(taskId: number): void {
    this.taskService.deleteTask(taskId).subscribe({
      next: () => {
        this.tasks.update((list) => list.filter((task) => task.id !== taskId));
        if (this.editingTask()?.id === taskId) {
          this.clearTaskEdit();
        }
      },
      error: () => this.setError('Unable to delete task.')
    });
  }
}
