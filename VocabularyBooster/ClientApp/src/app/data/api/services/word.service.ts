/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { Word } from '../models/word';
@Injectable({
  providedIn: 'root',
})
class WordService extends __BaseService {
  static readonly AddWordPath = '/api/Word/add';
  static readonly SearchWordPath = '/api/Word/search';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `WordService.AddWordParams` containing the following parameters:
   *
   * - `body`:
   *
   * - `api-version`: The requested API version
   *
   * @return The text.
   */
  AddWordResponse(params: WordService.AddWordParams): __Observable<__StrictHttpResponse<boolean>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.body;
    if (params.apiVersion != null) __params = __params.set('api-version', params.apiVersion.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Word/add`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'text'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return (_r as HttpResponse<any>).clone({ body: (_r as HttpResponse<any>).body === 'true' }) as __StrictHttpResponse<boolean>
      })
    );
  }
  /**
   * @param params The `WordService.AddWordParams` containing the following parameters:
   *
   * - `body`:
   *
   * - `api-version`: The requested API version
   *
   * @return The text.
   */
  AddWord(params: WordService.AddWordParams): __Observable<boolean> {
    return this.AddWordResponse(params).pipe(
      __map(_r => _r.body as boolean)
    );
  }

  /**
   * @param params The `WordService.SearchWordParams` containing the following parameters:
   *
   * - `phrase`:
   *
   * - `api-version`: The requested API version
   *
   * @return The word.
   */
  SearchWordResponse(params: WordService.SearchWordParams): __Observable<__StrictHttpResponse<Array<Word>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.phrase != null) __params = __params.set('phrase', params.phrase.toString());
    if (params.apiVersion != null) __params = __params.set('api-version', params.apiVersion.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Word/search`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<Word>>;
      })
    );
  }
  /**
   * @param params The `WordService.SearchWordParams` containing the following parameters:
   *
   * - `phrase`:
   *
   * - `api-version`: The requested API version
   *
   * @return The word.
   */
  SearchWord(params: WordService.SearchWordParams): __Observable<Array<Word>> {
    return this.SearchWordResponse(params).pipe(
      __map(_r => _r.body as Array<Word>)
    );
  }
}

module WordService {

  /**
   * Parameters for AddWord
   */
  export interface AddWordParams {
    body?: Word;

    /**
     * The requested API version
     */
    apiVersion?: string;
  }

  /**
   * Parameters for SearchWord
   */
  export interface SearchWordParams {
    phrase?: string;

    /**
     * The requested API version
     */
    apiVersion?: string;
  }
}

export { WordService }
