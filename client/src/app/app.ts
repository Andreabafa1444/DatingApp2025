import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Nav],
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = signal('Dating App');
  protected members = signal<any>([]);

  async ngOnInit(): Promise<void> {
    this.members.set(await this.getMembers());
  }

  async getMembers(): Promise<Object> {
    try {
      const result = await lastValueFrom(
        this.http.get('http://localhost:5001/api/members')
      );
      console.log("Miembros recibidos:", result);
      return result;
    } catch (error) {
      console.log("Error al obtener miembros:", error);
      throw error;
    }
  }
}
