export class CommentsModel {
  public id: number;
  public authorName: string;
  public text: string;

  constructor(authorName: string, text: string) {
    this.authorName = authorName;
    this.text = text;
  }
}
