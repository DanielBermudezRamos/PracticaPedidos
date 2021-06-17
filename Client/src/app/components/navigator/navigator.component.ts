import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/service/admin.service';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.css']
})
export class NavigatorComponent implements OnInit {

  constructor(private auth: AdminService) { }

  ngOnInit(): void {
  }
  onClose(): void {
    this.auth.logoutUser();
  }

}
