export interface TaskItem {
  id: number;
  title: string;
  startDate: string;
  endDate: string;
  assignedTo: string;
  owner: string;
  priority: string;
  status: string;
  boardId: number;
}
