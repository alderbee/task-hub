export class AddTask {
  userId: number;
  title: string;
  status: number;
  startDate?: Date;
  endDate?: Date;
  createdAt: Date;
  content?: string;
  priorityId: number;

  constructor(data: Partial<AddTask>) {
    this.userId = data.userId || 0;
    this.title = data.title || '';
    this.status = data.status || 0;
    this.startDate = data.startDate ? new Date(data.startDate) : undefined;
    this.endDate = data.endDate ? new Date(data.endDate) : undefined;
    this.createdAt = data.createdAt ? new Date(data.createdAt) : new Date();
    this.content = data.content;
    this.priorityId = data.priorityId || 0;
  }
}
