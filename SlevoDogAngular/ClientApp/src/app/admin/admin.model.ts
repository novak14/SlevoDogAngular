export class AdminModel {
  public Name: string;
  public PriceAfterSale: number;
  public AveragePrice: number;
  public OriginPrice: number;
  public Image: string;
  public ValidFrom: string;
  public ValidTo: string;
  public LinkFirm: string;
  public Description: string;
  public Disabled: boolean;
  public CheckedCategories: number[];

  constructor(Name: string,
              PriceAfterSale: number,
              AveragePrice: number,
              OriginalPrice: number,
              Image: string,
              ValidFrom: string,
              ValidTo: string,
              LinkFirm: string,
              Description: string,
              Disabled: boolean,
              CheckedCategories: number[]) {
    this.Name = Name;
    this.PriceAfterSale = PriceAfterSale;
    this.AveragePrice = AveragePrice;
    this.OriginPrice = OriginalPrice;
    this.Image = Image;
    this.ValidFrom = ValidFrom;
    this.ValidTo = ValidTo;
    this.LinkFirm = LinkFirm;
    this.Description = Description;
    this.Disabled = Disabled;
    this.CheckedCategories = CheckedCategories;
  }
}
