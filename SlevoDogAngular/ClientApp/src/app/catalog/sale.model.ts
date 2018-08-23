import {CommentsModel} from './comments.model';

export class Sale {
  public id: number;
  public name: string;
  public image: string;
  public priceAfterSale: string;
  public originPrice: number;
  public dateInsert: string;
  public validFrom: string;
  public validTo: string;
  public bDisabled: boolean;
  public linkFirm: string;
  public description: string;
  public percentSale: number;
  public saleCollection: SaleCollection;
  public rankSale: number;
  public categoryName: string;
  public comments: CommentsModel[];

  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
  }
}

export class SaleCollection {
  collections: Sale[];
}
