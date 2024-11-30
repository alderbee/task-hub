export class TaskModel {
  taskId: number;
  userId: number;
  title: string;
  status: number;
  startDate?: Date;
  endDate?: Date;
  createdAt: Date;
  updatedAt?: Date;
  content?: string;
  priorityId: number;

  constructor(data: Partial<TaskModel>) {
    this.taskId = data.taskId || 0;
    this.userId = data.userId || 0;
    this.title = data.title || '';
    this.status = data.status || 0;
    this.startDate = data.startDate ? new Date(data.startDate) : undefined;
    this.endDate = data.endDate ? new Date(data.endDate) : undefined;
    this.createdAt = data.createdAt ? new Date(data.createdAt) : new Date();
    this.updatedAt = data.updatedAt ? new Date(data.updatedAt) : undefined;
    this.content = data.content;
    this.priorityId = data.priorityId || 0;
  }
}
