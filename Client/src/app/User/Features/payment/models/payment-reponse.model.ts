export interface PaymentResponse {
  requestId: string;
  errorCode: number;
  orderId: string;
  message: string;
  localMessage: string;
  requestType: string;
  payUrl: string;
  signature: string;
  qrCodeUrl: string;
  deeplink: string;
  deeplinkWebInApp: string;
}