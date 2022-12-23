export enum AlertType {
  Success,
  Info,
  Warning,
  Danger,
}

export type Alert = {
  type: AlertType;
  message: string;
};
