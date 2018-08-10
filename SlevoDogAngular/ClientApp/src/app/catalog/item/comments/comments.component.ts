import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CookieService} from 'ngx-cookie-service';
import {CommentsModel} from '../../comments.model';
import {CatalogService} from '../../catalog.service';
import {FormControl, FormGroup, NgForm, Validators} from '@angular/forms';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  cookieValue = '';
  @ViewChild('f') commentForm: NgForm;
  @ViewChild('c') commentAnswerForm: NgForm;
  commentModel: CommentsModel;
  answerCommentForm: FormGroup;
  @Input() saleId: number;
  comments: CommentsModel[];
  gest: CommentsModel[];
  username: string;
  showFormAnswer = false;

  constructor( private cookieService: CookieService,
               private catalogService: CatalogService) { }

  async ngOnInit() {
    await this.initFormWithCookie();
  }

  async initFormWithCookie() {
    await this.refreshComments();
    const IsCookieExist = this.cookieService.get('UserComment');

    if (IsCookieExist) {
      await this.catalogService.getUserForComment(IsCookieExist).then(res => {
        this.cookieValue = res.toString();
      });
      this.commentForm.form.patchValue({
        username: this.cookieValue
      });
    }
  }

  async onSubmit() {
    this.commentModel = new CommentsModel(this.saleId, this.commentForm.value.username, this.commentForm.value.commentText);
    await this.catalogService.insertComment(this.commentModel).then(res => {
      this.cookieService.set('UserComment', res.toString());
    });
    this.commentForm.reset();
    await this.initFormWithCookie();
  }

  async onSubmitAnswer(parentCommentId: number) {
    this.commentModel = new CommentsModel(this.saleId,
      this.answerCommentForm.value.responseUsername,
      this.answerCommentForm.value.commentText);

    this.commentModel.fkParrentComment = parentCommentId;
    await this.catalogService.insertComment(this.commentModel).then(res => {
      this.cookieService.set('UserComment', res.toString());
    });
    this.commentForm.reset();
    await this.initFormWithCookie();
  }

  async refreshComments() {
    await this.catalogService.getComments(this.saleId).then(res => {
      this.comments = res;
    });
  }

  fillAnswer(comm: CommentsModel) {
    this.answerCommentForm = new FormGroup({
      'responseUsername': new FormControl(this.cookieValue, Validators.required),
      'commentText': new FormControl(null, Validators.required)
    });

    for (const comment of this.comments) {
      if (comment.check) {
        comment.check = false;
      }
    }
    this.showFormAnswer = true;
    comm.check = true;

  }
}
