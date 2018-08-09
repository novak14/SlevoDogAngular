export class CommentsModel {
  public id: number;
  public name: string;
  public text: string;
  public dateInsert: string;
  public rank: string;
  public fkParrentComment: number;
  public check: boolean;

  constructor(id: number, authorName: string, text: string) {
    this.id = id;
    this.name = authorName;
    this.text = text;
    this.check = false;
  }
}
