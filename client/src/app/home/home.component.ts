import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  user:any
  regisMode = false
  constructor(private http:HttpClient){}
  ngOnInit(): void{

  }

  regisToggle() {
    this.regisMode = !this.regisMode
  }

  cancelRegister(even:Boolean){
    this.regisMode = !event
  }
}
