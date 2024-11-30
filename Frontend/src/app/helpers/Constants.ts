export enum ErrorMessages {
  FormValidation = 'All fields are required',
  RegistrationFormEmail = 'Valid Email Id is required',
  UnknownError = 'Please try again later or contact support. Soory for inconvenience',
  AddNewTaskError = 'Error while adding new task. Please try again later or contact customer support',
  AddNewTaskValidatorMessage = 'All the fields are required',
  DeleteTaskMessage = 'Failed to delete task',
}

export enum Messages {
  NoDataSaved = 'Dialog was closed without saving data.',
  TaskDeleted = 'Task deleted successfully',
  PasswordRequirementMessage = 'At least 8 characters in length, Lowercase letter, Uppercase letter, Number and Special character',
  EditTask = 'Edit Task',
  AddNewTask = 'Add New Task',
}

export enum OperationType {
  AddNewTask = 'AddNewTask',
  UpdateExistingTask = 'UpdateExistingTask',
}

export const PriorityOptions = [
  { id: 1, label: 'Low' },
  { id: 2, label: 'Normal' },
  { id: 3, label: 'High' },
];

export const StatusOptions = [
  { id: 1, label: 'To Do' },
  { id: 2, label: 'In Progress' },
  { id: 3, label: 'On Hold' },
  { id: 4, label: 'Completed' },
];
