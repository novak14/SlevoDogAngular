import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {Sale} from './sale.model';
import {CommentsModel} from './comments.model';
import { Subject } from 'rxjs';

@Injectable()
export class CatalogService {
  baseUrl: string;
  testas: CommentsModel[];
  browse: Sale;
  browseChanged = new Subject<Sale>();

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

   async getItems(sortValue: string) {
    this.browse = await this.http.get<Sale>(this.baseUrl + 'api/Catalog/AllItemsAsync', {
      params: new HttpParams().set('sortOrder', sortValue)
    }).toPromise();
    this.browseChanged.next(this.browse);
    return this.browse;
  }

  async getCategoryItems(categoryId: number, sortValue: string) {
    this.browse = await this.http.get<Sale>(this.baseUrl + 'api/Catalog/CategoryItemsAsync', {
      params: new HttpParams().set('categoryId', String(categoryId)).set('sortOrder', sortValue)
    }).toPromise();
    this.browseChanged.next(this.browse);
    return this.browse;
  }

  async getSale(id: number) {
    return await this.http.get<Sale>(this.baseUrl + 'api/Item/ItemAsync', {
      params: new HttpParams().set('id', String(id))
    }).toPromise();
  }

  async addRankToSale(saleId: number, rank: number) {
    console.log('S: ' + saleId + ' R: ' + rank);
    return await this.http.put(this.baseUrl + 'api/Item/RankSale', {
      id: saleId,
      rankSale: rank
    }).toPromise();
  }

  async decreaseRankToSale(saleId: number, rank: number) {
    return await this.http.put(this.baseUrl + 'api/Item/RankSale', {
      id: saleId,
      rankSale: rank
    }).toPromise();
  }

  async insertComment(comment: CommentsModel) {
    return await this.http.post(this.baseUrl + 'api/Item/AddCommentsAsync', comment).toPromise();
  }

  async getUserForComment() {
    return await this.http.get(this.baseUrl + 'api/Item/GetUserNameCommentAsync').toPromise();
  }

  async getComments(saleId: number) {
    return await this.http.get<CommentsModel[]>(this.baseUrl + 'api/Item/GetCommentsAsync', {
      params: new HttpParams().set('saleId', String(saleId))
    }).toPromise();
  }

  async addRankToComment(commentId: number, rank: number) {
    return await this.http.put(this.baseUrl + 'api/Item/RankComment', {
      id: commentId,
      rank: rank
    }).toPromise();
  }
}
