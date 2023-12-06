import { Component, EventEmitter, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  @Output() isCancel = new EventEmitter()

  constructor(private router: Router, private accountServicce: AccountService){}

  model: any = {}

  register() {
    this.accountServicce.register(this.model).subscribe({
      error: err => console.log(err),
      next:()  =>  this.router.navigateByUrl('/members')
    })
  }

  cancel() {
    this.isCancel.emit(true)
  }
}
