// นำเข้า Injectable จาก Angular core
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

// ใช้ Injectable decorator เพื่อทำให้ class เป็น Angular service และลงทะเบียนเป็น Singleton ใน root injector
@Injectable({
    providedIn: 'root'
})
export class AccountService {
    // กำหนด baseUrl สำหรับเป็นที่ตั้งของ API ที่ให้บริการ
    baseUrl = 'https://localhost:7777/api/'

    // กำหนด constructor ที่รับ dependency จาก HttpClient
    private currentUserSource = new BehaviorSubject<User | null>(null)
    currentUser$ = this.currentUserSource.asObservable()
    
    constructor(private http: HttpClient) { }

    register(model: any) {
        return this.http.post<User>(`${this.baseUrl}account/register`, model).pipe(
            map(user => {
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user))
                    this.currentUserSource.next(user)
                }
            })
        )
    }
    // เมทอด login ใน class AccountService
    login(model: any) {
        // ใช้ HttpClient ในการทำ HTTP POST request ไปยัง API สำหรับการล็อกอิน
        return this.http.post<User>(`${this.baseUrl}account/login`, model).pipe(
            map((user: User) => {
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user))
                    this.currentUserSource.next(user)
                }
            })
        )
    }
    logout() {
        localStorage.removeItem('user')
        this.currentUserSource.next(null)
    }
    setCurrentUser(user: User) {
        this.currentUserSource.next(user)
    }

}
