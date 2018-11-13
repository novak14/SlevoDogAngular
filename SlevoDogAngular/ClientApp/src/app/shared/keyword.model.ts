export class KeywordModel {
    public id: number;
    public keyword: string;
    public fullKeyword: string;

    constructor(id: number, keyword: string, fullKeyword: string) {
      this.id = id;
      this.keyword = keyword;
      this.fullKeyword = fullKeyword;
    }
  }
