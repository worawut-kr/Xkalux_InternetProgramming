import { Component, OnInit } from '@angular/core'
import { AccountService } from '../_services/account.service'
import { User } from '../_models/user'
import { Observable, of } from 'rxjs'
import { ToastrService } from 'ngx-toastr'
import { Router } from '@angular/router'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  currentUser$: Observable<User | null> = of(null) // isLogin = false

  constructor(private toastr: ToastrService,private router : Router, private accountService: AccountService) { }
  
  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
        next: user => console.log(user), // user?true:false
        error: err => console.log(err)
    })
  }
  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
  }

  login(): void {
      this.accountService.login(this.model).subscribe({ //Observable
          next: response => {
              this.router.navigateByUrl('/members')
          },
          error: err => this.toastr.error(err.error) //anything that's not in 200 range of HTTP status
      })
  }
  logout() {
    this.accountService.logout()
    this.model={
      username: '',
      password: ''
    }
    this.router.navigateByUrl('/');
  }
}
