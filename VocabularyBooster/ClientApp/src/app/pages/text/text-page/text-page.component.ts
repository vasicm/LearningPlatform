import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Text } from 'src/app/data/api/models';
import { TextService } from 'src/app/data/api/services';

@Component({
  templateUrl: './text-page.component.html',
  styleUrls: ['./text-page.component.css']
})
export class TextPageComponent implements OnInit {
  form: FormGroup;
  textList: Text[];

  constructor(
    private textService: TextService
  ) { }

  ngOnInit() {
    this.form = new FormGroup({
      query: new FormControl("", Validators.minLength(1))
    });
  }

  submit() {
    this.textService.SearchText({
      phrase: this.form.getRawValue().query.trim()
    }).subscribe(x => {
      this.textList = x;
    })
  }
}
