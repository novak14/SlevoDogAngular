import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CookieService} from 'ngx-cookie-service';
import {CommentsModel} from '../../comments.model';
import {CatalogService} from '../../catalog.service';
import {FormControl, FormGroup, NgForm, Validators} from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css', '../../catalog.component.css']
})
export class CommentsComponent implements OnInit {
  userName = '';
  @ViewChild('f') commentForm: NgForm;
  @ViewChild('c') commentAnswerForm: NgForm;
  commentModel: CommentsModel;
  answerCommentForm: FormGroup;
  @Input() saleId: number;
  comments: CommentsModel[];
  gest: CommentsModel[];
  username: string;
  showFormAnswer = false;
  existUser = false;

  constructor( private cookieService: CookieService,
               private catalogService: CatalogService,
               private jwtHelper: JwtHelperService,
              private authService: AuthService) { }

  async ngOnInit() {
    await this.initForm();
  }

  async initForm() {
    await this.refreshComments();

    if (this.authService.isAuthenticated()) {
      this.existUser = true;
      await this.catalogService.getUserForComment().then(res => {
        this.userName = res.toString();
      });
      this.commentForm.form.patchValue({
        username: this.userName
      });
    }
  }

  async onSubmit() {
    if (this.authService.isAuthenticated()) {
      this.commentModel = new CommentsModel(this.saleId, this.commentForm.value.username, this.commentForm.value.commentText);
      await this.catalogService.insertComment(this.commentModel).then(res => {
        console.log('V poradku');
      });
      this.commentForm.reset();
      await this.initForm();
    }
  }

  async onSubmitAnswer(parentCommentId: number) {
    if (this.authService.isAuthenticated()) {
      this.commentModel = new CommentsModel(this.saleId,
        this.answerCommentForm.value.responseUsername,
        this.answerCommentForm.value.commentText);

      this.commentModel.fkParrentComment = parentCommentId;
      await this.catalogService.insertComment(this.commentModel).then(res => {
        console.log('V poradku');
      });
      this.showFormAnswer = false;
      this.answerCommentForm.reset();
      await this.initForm();
    }
  }

  async refreshComments() {
    console.log('SaleId: ' + this.saleId);
    await this.catalogService.getComments(this.saleId).then(res => {
      this.comments = res;
    });
  }

  fillAnswer(comm: CommentsModel) {
    if (this.authService.isAuthenticated()) {
      this.answerCommentForm = new FormGroup({
        'responseUsername': new FormControl(this.userName, Validators.required),
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

  async addRank(commId: number) {
    if (this.authService.isAuthenticated()) {
      const comment = this.comments.find( tes => tes.id === commId);
      const rank = comment.rank + 1;
      await this.catalogService.addRankToComment(commId, Number(rank))
        .then( res => {
      })
        .catch(console.log);

      comment.rank += 1;
      }
    }
}
