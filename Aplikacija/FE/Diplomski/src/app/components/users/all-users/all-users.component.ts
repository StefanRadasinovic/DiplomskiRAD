import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { User, UserInfo } from '../../../models/userDTO';
import { UserService } from '../../../services/userService';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrl: './all-users.component.css'
})
export class AllUsersComponent implements AfterViewInit {

  displayedColumns: string[] = ['name', 'surname', 'username', 'role'];
  isLoading = true;
  users: UserInfo[] = [];
  currentUser: UserInfo = {
    id: '',
    name: '',
    surname: '',
    username: '',
    role: ''
  };

  currentIndex = -1;
  surname = '';
  name = '';
  username = '';
  role = '';

  constructor(private userService: UserService, private router: Router) { }

  ngAfterViewInit(): void {
    this.retrieveUsers();
  }

  retrieveUsers(): void {
    this.userService.getAllUsers()
      .subscribe({
        next: (data) => {
          this.users = data;
          this.isLoading = false;
        },
        error: (e) => console.error(e)
      });
  }

  setActiveUsers(user: UserInfo, index: number): void {
    this.currentUser = user;
    this.currentIndex = index;
    this.router.navigate([`/display/${user.id}`]); // Navigate to the user profile
  }

  filterByNameAndSurname(): void {
    this.userService.getAllUsers()
      .subscribe({
        next: (data) => {
          this.users = data.filter(centre => 
            centre.name?.includes(this.name.toLowerCase()) &&
            centre.surname?.includes(this.surname.toLowerCase()) &&
            centre.username?.includes(this.username.toLowerCase()) &&
            centre.role?.includes(this.role)
          );
        },
        error: (e) => console.error(e)
      });
  }

}