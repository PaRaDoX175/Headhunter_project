import { Component, OnInit, ViewChild } from '@angular/core';
import { IResponse, IResume } from '../interfaces/appUser';
import { AccountService } from '../services/account.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  @ViewChild('resumeForm') resumeForm!: NgForm;
  resume?: IResume;
  profileInfo?: IResponse;
  displayName: string | null = localStorage.getItem('DisplayName');
  token: string | null = localStorage.getItem('TOKEN');
  flag = false;

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
    this.getResume();
    this.getProfileInfo();
  }

  getResume() {
    this.accountService.getResume().subscribe({
      next: (data) => {
        this.resume = data;
        console.log(this.resume);
      },
    });
  }

  updateResume(resume: any) {
    this.accountService.updateResume(this.token, resume)?.subscribe({
      next: (data) => {
        console.log(data);
        this.resume = data;
        window.location.reload();
      },
    });
  }

  getProfileInfo() {
    const token = localStorage.getItem('TOKEN');

    if (token) {
      this.accountService.getLoginInfo(token)?.subscribe({
        next: (data) => {
          this.profileInfo = data;
          console.log(data);
        },
      });
    }
  }

  logout() {
    localStorage.removeItem('TOKEN');
    localStorage.removeItem('DisplayName');
    localStorage.removeItem('BasketId');
    this.router.navigate(['']).then(() => {
      window.location.reload();
    });
  }

  openModal() {
    this.flag = true;
  }

  closeModal() {
    this.flag = false;
  }
}
