import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private data: Map<string, any> = new Map();

  public put(id: string, data: any): void {
    this.data.set(id, data);
  }

  public get(id: string): any {
    return this.data.get(id);
  }

  public remove(id: string): boolean {
    return this.data.delete(id);
  }
}
