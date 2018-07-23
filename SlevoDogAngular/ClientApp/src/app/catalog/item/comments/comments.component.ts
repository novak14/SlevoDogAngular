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

  constructor( private cookieService: CookieService,
               private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.cookieService.set( 'Test', 'Hello World' );
    this.cookieValue = this.cookieService.get('Test');
  }

  testCookie() {
    const test = this.cookieService.get('Test');
    console.log('Test: ' + test);
  }

  sendToServer() {
    this.catalogService.insertComment(this.comment);
    this.cookieService.get('Id');
  }

}
