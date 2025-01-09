import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  token: string | null = '';
  displayName: string | null = '';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('TOKEN');
    this.displayName = localStorage.getItem('DisplayName');
  }

  updateData() {
    if (this.router.url.includes('catalog')) {
      this.router.navigate(['catalog']).then(() => {
        window.location.reload();
      });
    }
  }
}
