export interface PaginatedResult<T> {
  pageIndex: number;
  totalPages: number;
  items: T[];
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
