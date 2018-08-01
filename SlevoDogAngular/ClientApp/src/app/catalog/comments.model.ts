export class CommentsModel {
  public id: number;
  public name: string;
  public text: string;
  public dateInsert: string;
  public rank: string;

  constructor(id: number, authorName: string, text: string) {
    this.id = id;
    this.name = authorName;
    this.text = text;
  }
}
