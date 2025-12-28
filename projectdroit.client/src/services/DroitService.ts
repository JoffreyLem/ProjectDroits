import { ApiResponseError, ApiService } from './ApiService';

export enum FondApiName {
  CODE = 'CODE',
  LEGI = 'LEGI',
  KALI = 'KALI',
  JORF = 'JORF',
}
export interface PromptContentDto {
  content: string;
  isAdvancedSearch: boolean;
  selectedFonds?: FondApiName[];
}

export class DroitService {
  private static readonly baseUrl = '/Api/Droit';

  static async GlobalSearch(
    promptContent: PromptContentDto,
  ): Promise<string | ApiResponseError> {
    return ApiService.Post<string>(`${this.baseUrl}/SearchLaw`, promptContent);
  }
}
