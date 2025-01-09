import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'HeadHunter';
  isExpired: boolean = false;

  constructor(private router: Router, private accountService: AccountService) {}

  ngOnInit(): void {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isTokenExpired();
      });
  }

  reLogin() {
    localStorage.removeItem('TOKEN');
    localStorage.removeItem('DisplayName');
    localStorage.removeItem('BasketId');
    this.isExpired = false;
    this.router.navigate(['login']).then(() => window.location.reload);
  }

  isTokenExpired() {
    this.accountService.isTokenExpired().subscribe({
      next: (res) => {
        this.isExpired = res;
        console.log(this.isExpired);
      },
    });
  }
}
