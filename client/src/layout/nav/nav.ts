import { Component, inject, Inject } from '@angular/core';
import { AccountService } from '../../core/services/account-service';

import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
      private accountService = inject(AccountService);


  protected creds: any = {};
login(): void {
    console.log(this.creds);
    this.accountService.login(this.creds).subscribe(
      {
        next: response => console.log(JSON.stringify(response)),
        error: error => alert(error.message)
      }
    );
  }
}