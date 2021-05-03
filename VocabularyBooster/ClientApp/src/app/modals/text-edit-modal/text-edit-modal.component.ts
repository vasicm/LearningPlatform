import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalCloseService } from 'src/app/common/modal-close.service';
import { Text } from 'src/app/data/api/models';
import { TextService } from 'src/app/data/api/services';

@Component({
  selector: 'vb-text-edit-modal',
  templateUrl: './text-edit-modal.component.html',
  styleUrls: ['./text-edit-modal.component.css']
})
export class TextEditModalComponent implements OnInit {
  data: {
    text: Text;
  };
  form: FormGroup;
  constructor(
    private textService: TextService,
    private modalClose: ModalCloseService
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

  close() {
    this.modalClose.hide();
  }
}
