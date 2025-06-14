export interface ProductFilter {
  status?: string;
  brandId?: string;
  size?: string;
  minPrice?: number;
  maxPrice?: number;
  pageIndex: number;
  pageSize: number;
}
