import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { LoginModel } from 'src/app/Models/login.model';
import { AuthService } from 'src/app/Services/Auth.service';
import jwt_decode from "jwt-decode";

@Component({
  selector: 'Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private notifierService: NotifierService,
     private router: Router,
     private authService: AuthService) { }

  model = new LoginModel();
  token_data: any;

  submitLogin(){
    if(!this.model.isValid()){
      this.notifierService.notify("error", "Please, enter all fields!")
    }
    else if(!this.model.isEmail()){
      this.notifierService.notify("error", "Please, enter correct email!")
    }
    else{
      this.authService.login(this.model).subscribe(data=>{
        if(data.code == 200){
          this.notifierService.notify("success", "You have successfully logined!");
          this.router.navigate(['/']);
          localStorage.setItem("token", data.token);
          this.token_data = jwt_decode(data.token);

          if(this.token_data.roles == "Admin") {
            this.router.navigate(['/admin-panel'])
          }
          else if(this.token_data.roles == "User"){
            
            this.router.navigate(['/user-profile'])
          }
        }
        else{
          for(var i = 0; i < data.errors.length; i++) {
            this.notifierService.notify("error", data.errors[i]);
          }
        }
      });
    }
  }

  ngOnInit() {
  }

}
