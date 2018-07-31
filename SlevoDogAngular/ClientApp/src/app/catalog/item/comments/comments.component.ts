import {Component, OnInit, ViewChild} from '@angular/core';
import {CookieService} from 'ngx-cookie-service';
import {CommentsModel} from '../../comments.model';
import {CatalogService} from '../../catalog.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  cookieValue = 'UNKNOWN';
  @ViewChild('f') comment: NgForm;
  commentModel: CommentsModel;
  // public comment = new CommentsModel(32, 'Maca', 'Super');
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

  // sendToServer() {
  //   console.log('In Comment: ' + this.username);
  //   this.catalogService.insertComment(this.comment);
  //   const IsCookieExist = this.cookieService.get('UserComment');
  // }

  onSubmit() {
    this.commentModel = new CommentsModel(this.comment.value.username, this.comment.value.commentText);
    console.log('CommentMOdel: ' + this.commentModel.text);
    this.catalogService.insertComment(this.commentModel);
    this.comment.reset();

  }

}
