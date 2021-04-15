import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { RegisterModel } from 'src/app/Models/register.model';
import { AuthService } from 'src/app/Services/Auth.service';

@Component({
  selector: 'Register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(
    private notifierService: NotifierService,
     private router: Router,
     private authService: AuthService) { }

  model = new RegisterModel();

  submitRegister(){
    if(!this.model.isValid()){
      this.notifierService.notify("error", "Please, enter all fields!")
    }
    else if(this.model.Password != this.model.ConfirmPassword){
      this.notifierService.notify("error", "Password don't match!")
    }
    else if(!this.model.isEmail()){
      this.notifierService.notify("error", "Please, enter correct email!")
    }
    else{
      this.authService.register(this.model).subscribe(data=>{
        if(data.code == 200){
          this.notifierService.notify("success", "You have successfully registered!")
          this.router.navigate(['/login'])
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
