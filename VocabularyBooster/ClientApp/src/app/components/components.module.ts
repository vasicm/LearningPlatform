import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WordComponent } from './word/word.component';
import { WordEditComponent } from './word-edit/word-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextComponent } from './text/text.component';
import { TextEditComponent } from './text-edit/text-edit.component';

@NgModule({
  declarations: [
    WordComponent,
    WordEditComponent,
    TextComponent,
    TextEditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    WordComponent,
    WordEditComponent,
    TextComponent,
    TextEditComponent
  ]
})
export class ComponentsModule { }
