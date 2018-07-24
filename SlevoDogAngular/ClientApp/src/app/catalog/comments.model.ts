export class CommentsModel {
  public id: number;
  public authorName: string;
  public text: string;

  constructor(id: number, authorName: string, text: string) {
    this.id = id;
    this.authorName = authorName;
    this.text = text;
  }
}
