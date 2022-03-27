import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private http: HttpClient) {

  }

  users: any;
  title = "The Dating App";

  ngOnInit() {
    this.getUsers();
    
    
  }
  
  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(
      response => {
        this.users = response;
      },
      error => {
        console.log(error);
      }
    )
    console.log(this.users);
  }

}
