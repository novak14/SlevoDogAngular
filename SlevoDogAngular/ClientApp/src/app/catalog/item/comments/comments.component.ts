import { Component, OnInit } from '@angular/core';
import {CookieService} from 'ngx-cookie-service';
import {CommentsModel} from '../../comments.model';
import {CatalogService} from '../../catalog.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  cookieValue = 'UNKNOWN';
  public comment = new CommentsModel(32, 'Maca', 'Super');
  username: string;

  constructor( private cookieService: CookieService,
               private catalogService: CatalogService) { }

  ngOnInit(): void {
    const IsCookieExist = this.cookieService.get('UserComment');
    console.log('Usernames: ' + this.username);
    if (IsCookieExist) {
      console.log('Exist: ' + IsCookieExist);
      this.catalogService.getUserForComment(IsCookieExist).subscribe((res) => {
        this.cookieValue = res.toString();
      });
    }
  }

  testCookie() {
    const test = this.cookieService.get('Test');
    console.log('Test: ' + test);
  }

  sendToServer() {
    console.log('In Comment: ' + this.username);
    this.catalogService.insertComment(this.comment);
    const IsCookieExist = this.cookieService.get('UserComment');

  }

}
