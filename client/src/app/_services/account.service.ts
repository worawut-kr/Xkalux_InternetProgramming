// นำเข้า Injectable จาก Angular core
import { Injectable } from '@angular/core';

// ใช้ Injectable decorator เพื่อทำให้ class เป็น Angular service และลงทะเบียนเป็น Singleton ใน root injector
@Injectable({
    providedIn: 'root'
})
export class AccountService {
    // กำหนด baseUrl สำหรับเป็นที่ตั้งของ API ที่ให้บริการ
    baseUrl = 'https://localhost:7777/api/'

    // กำหนด constructor ที่รับ dependency จาก HttpClient
    constructor(private http: HttpClient) { }

    // เมทอด login ใน class AccountService
    login(model: any) {
        // ใช้ HttpClient ในการทำ HTTP POST request ไปยัง API สำหรับการล็อกอิน
        return this.http.post(`${this.baseUrl}account/login`, model)
    }
}
