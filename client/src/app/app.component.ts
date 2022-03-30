import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(/*private http: HttpClient,*/ private accountService: AccountService) {

  }

  users: any;
  title = "The Dating App";

  ngOnInit() {
    /*this.getUsers();*/
    this.setCurrentUser();
    
  }
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  /*getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(
      response => {
        this.users = response;
      },
      error => {
        console.log(error);
      }
    )
    console.log(this.users);
  }*/

}
