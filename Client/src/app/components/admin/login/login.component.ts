import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/domain/user';
import { AdminService } from 'src/app/service/admin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AdminService,
              private router: Router) { }
  /*private user: User = {
    email: '',
    password: ''
  };*/
  public isError = false;
  form!: FormGroup;

  ngOnInit(): void {
    this.initForm();
   }
  initForm(): void{
    this.form = new FormGroup({
      email: new FormControl('bermudez@example.com', [Validators.required]),
      clave: new FormControl('B3rm5d3z*', [Validators.required])
    })
  }

  onLogin() {
    if (this.form.valid) {
      this.authService
        .loginUser(this.form.value)
        .subscribe((data: any) => {
          console.log('Retorno de Login:');
          console.log(data);
          if(data.exitoso){
            this.authService.setToken(data.token);
            this.router.navigate(['/']);
          }
          /*this.authService.setUser(data.user);
          const token = data.id;
          this.authService.setToken(token);
          location.reload();
          this.isError = false;*/
        },
        (error) => {
          console.log(error);
          this.onIsError();}
        );
    } else {
      this.onIsError();
    }
  }

  onIsError(): void {
    this.isError = true;
    setTimeout(() => {
      this.isError = false;
    }, 4000);
  }
}
