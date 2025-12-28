import axios, { AxiosError, AxiosResponse } from 'axios';

export interface ApiResponseError {
  error?: string;
}

export class ApiService {
  private static api = axios.create({
    headers: {
      'Content-Type': 'application/json',
    },
  });

  private static handleError(error: any): never {
    let errorMessage = "Une erreur inconnue et inattendue s'est produite.";

    if (axios.isAxiosError(error)) {
      const axiosError = error as AxiosError<any>;
      if (axiosError.response) {
        errorMessage =
          axiosError.response.data?.error ||
          `Erreur API (Statut ${axiosError.response.status})`;
        console.error(
          `API Error ${axiosError.response.status}:`,
          axiosError.response.data,
        );
      } else if (axiosError.request) {
        errorMessage = 'Erreur réseau : Impossible de joindre le serveur.';
        console.error('Network/Request Error:', axiosError.message);
      } else {
        errorMessage = `Erreur lors de la préparation de la requête : ${axiosError.message}`;
        console.error('Axios Setup Error:', axiosError.message);
      }
    } else if (error instanceof Error) {
      errorMessage = `Une erreur inattendue s'est produite : ${error.message}`;
      console.error('Generic Error:', error);
    } else {
      console.error('Unknown Throwable:', error);
    }

    throw new Error(errorMessage);
  }

  static async Get<T>(url: string): Promise<T | ApiResponseError> {
    try {
      const response: AxiosResponse<T> = await this.api.get(url);
      return response.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  static async Post<T>(url: string, data?: any): Promise<T | ApiResponseError> {
    try {
      const response: AxiosResponse<T> = await this.api.post(url, data);
      return response.data;
    } catch (error) {
      this.handleError(error);
    }
  }
}
