import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from './register.service';
import { Router } from '@angular/router';
import { IErrors, IResponse } from '../interfaces/errors';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  constructor(
    private registerService: RegisterService,
    private router: Router
  ) {}

  errors: string[] = [];

  profileForm: FormGroup = new FormGroup({
    displayName: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    phoneNumber: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.email, Validators.required]),
  });

  resumeForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    aboutMe: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.email, Validators.required]),
    phoneNumber: new FormControl('', Validators.required),
    profession: new FormControl('', Validators.required),
    skills: new FormControl('', Validators.required),
  });

  onSubmit() {
    this.registerService
      .sendData(this.profileForm.value, this.resumeForm.value)
      .subscribe({
        next: (data) => {
          if (data.token) {
            localStorage.setItem('TOKEN', data.token);
            localStorage.setItem('DisplayName', data.displayName);
            this.router.navigateByUrl('').then(() => {
              window.location.reload();
            });
          }
        },
        error: (data: IResponse) => {
          this.errors = data.error.errors;
        },
      });
  }
}
