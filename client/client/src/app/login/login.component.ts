import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  constructor(private accountService: AccountService, private router: Router) {}

  logInForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  onSubmit() {
    this.accountService.login(this.logInForm.value).subscribe({
      next: (data) => {
        localStorage.setItem('TOKEN', data.token);
        localStorage.setItem('DisplayName', data.displayName);
        localStorage.setItem('BasketId', data.basketId);
        this.router.navigate(['']).then(() => {
          window.location.reload();
        });
      },
    });
  }
}
