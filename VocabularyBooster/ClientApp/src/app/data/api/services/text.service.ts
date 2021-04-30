/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { Text } from '../models/text';
@Injectable({
  providedIn: 'root',
})
class TextService extends __BaseService {
  static readonly AddTextPath = '/api/Text/add';
  static readonly SearchTextPath = '/api/Text/search';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `TextService.AddTextParams` containing the following parameters:
   *
   * - `body`:
   *
   * - `api-version`: The requested API version
   *
   * @return The text.
   */
  AddTextResponse(params: TextService.AddTextParams): __Observable<__StrictHttpResponse<boolean>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.body;
    if (params.apiVersion != null) __params = __params.set('api-version', params.apiVersion.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Text/add`,
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
   * @param params The `TextService.AddTextParams` containing the following parameters:
   *
   * - `body`:
   *
   * - `api-version`: The requested API version
   *
   * @return The text.
   */
  AddText(params: TextService.AddTextParams): __Observable<boolean> {
    return this.AddTextResponse(params).pipe(
      __map(_r => _r.body as boolean)
    );
  }

  /**
   * @param params The `TextService.SearchTextParams` containing the following parameters:
   *
   * - `phrase`:
   *
   * - `api-version`: The requested API version
   *
   * @return The text.
   */
  SearchTextResponse(params: TextService.SearchTextParams): __Observable<__StrictHttpResponse<Array<Text>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.phrase != null) __params = __params.set('phrase', params.phrase.toString());
    if (params.apiVersion != null) __params = __params.set('api-version', params.apiVersion.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Text/search`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<Text>>;
      })
    );
  }
  /**
   * @param params The `TextService.SearchTextParams` containing the following parameters:
   *
   * - `phrase`:
   *
   * - `api-version`: The requested API version
   *
   * @return The text.
   */
  SearchText(params: TextService.SearchTextParams): __Observable<Array<Text>> {
    return this.SearchTextResponse(params).pipe(
      __map(_r => _r.body as Array<Text>)
    );
  }
}

module TextService {

  /**
   * Parameters for AddText
   */
  export interface AddTextParams {
    body?: Text;

    /**
     * The requested API version
     */
    apiVersion?: string;
  }

  /**
   * Parameters for SearchText
   */
  export interface SearchTextParams {
    phrase?: string;

    /**
     * The requested API version
     */
    apiVersion?: string;
  }
}

export { TextService }
