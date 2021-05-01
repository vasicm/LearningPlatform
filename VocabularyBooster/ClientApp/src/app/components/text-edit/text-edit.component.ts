import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Text } from 'src/app/data/api/models';
import { TextService } from 'src/app/data/api/services';

@Component({
  selector: 'vb-text-edit',
  templateUrl: './text-edit.component.html',
  styleUrls: ['./text-edit.component.css']
})
export class TextEditComponent implements OnInit {
  @Input('text') text: Text;
  form: FormGroup;

  constructor(
    private textService: TextService
  ) { }

  ngOnInit() {
    this.form = new FormGroup({
      title: new FormControl("", Validators.minLength(1)),
      author: new FormControl(""),
      publisher: new FormControl(""),
      topic: new FormControl(""),
      type: new FormControl(""),
      content: new FormControl("")
    });
  }

  submit() {
    debugger;
    this.textService.AddText({
      body: {
        title: this.form.getRawValue().title.trim(),
        author: this.form.getRawValue().author.trim(),
        publisher: this.form.getRawValue().publisher.trim(),
        topic: this.form.getRawValue().topic.trim(),
        type: 0,
        content: this.form.getRawValue().content.trim(),
      }
    }).subscribe();
  }
}
