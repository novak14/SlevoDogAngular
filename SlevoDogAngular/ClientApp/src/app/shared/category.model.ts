export class CategoryModel {
  public id: number;
  public name: string;
  public disabled: boolean;
  public fkParentCategory: number;
  public checkCategory: boolean;

  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
  }
}
