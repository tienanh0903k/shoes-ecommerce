import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AdminheaderComponent } from '../../admin/Core/adminheader/adminheader.component';

@Component({
  selector: 'app-admin-layout',
  imports: [RouterOutlet, AdminheaderComponent],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css',
  
})
export class AdminLayoutComponent {
 
}
