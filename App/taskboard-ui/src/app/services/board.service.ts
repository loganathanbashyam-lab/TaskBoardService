import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from '../models/board';

@Injectable({ providedIn: 'root' })
export class BoardService {
  constructor(private http: HttpClient) {}

  getBoardsForUser(userId: number): Observable<Board[]> {
    return this.http.get<Board[]>(`/api/boards/user/${userId}`);
  }

  createBoard(name: string, userId: number): Observable<Board> {
    return this.http.post<Board>('/api/boards', { name, userId });
  }

  deleteBoard(boardId: number): Observable<void> {
    return this.http.delete<void>(`/api/boards/${boardId}`);
  }
}
