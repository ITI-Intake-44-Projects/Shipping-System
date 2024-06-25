import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {

  constructor() { }

  log(message: string): void {
    console.log(`[LoggerService]: ${message}`);
  }

  error(message: string): void {
    console.error(`[LoggerService]: ${message}`);
  }

  warn(message: string): void {
    console.warn(`[LoggerService]: ${message}`);
  }
}
