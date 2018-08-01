import {Component, Input, OnInit, ViewChild} from '@angular/core';
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
  @ViewChild('f') commentForm: NgForm;
  commentModel: CommentsModel;
  @Input() saleId: number;
  @Input() comments: CommentsModel[];
  gest: CommentsModel[];
  username: string;

  constructor( private cookieService: CookieService,
               private catalogService: CatalogService) { }

  ngOnInit(): void {
    const IsCookieExist = this.cookieService.get('UserComment');
    if (IsCookieExist) {
      this.commentForm.form.patchValue({
        userData: {
          username: IsCookieExist
        }
      });
      console.log('Exist: ' + IsCookieExist);
      this.catalogService.getUserForComment(IsCookieExist).subscribe((res) => {
        this.cookieValue = res.toString();
      });
    }
  }

  async onSubmit() {
    this.commentModel = new CommentsModel(this.saleId, this.commentForm.value.username, this.commentForm.value.commentText);
    await this.catalogService.insertComment(this.commentModel).then(res => {
      this.cookieService.set('UserComment', res.toString());
    });
    this.commentForm.reset();
    await this.refreshComments();
  }

  async refreshComments() {
    await this.catalogService.getComments(this.saleId).then(res => {
      this.comments = res;
    });
  }
}
