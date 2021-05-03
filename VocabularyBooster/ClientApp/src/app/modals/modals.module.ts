import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from '../components/components.module';
import { TextEditModalComponent } from './text-edit-modal/text-edit-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { WordEditModalComponent } from './word-edit-modal/word-edit-modal.component';



@NgModule({
  declarations: [
    TextEditModalComponent,
    WordEditModalComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule
  ],
  exports: [
    TextEditModalComponent,
    WordEditModalComponent
  ],
  entryComponents: [
    TextEditModalComponent,
    WordEditModalComponent
  ]
})
export class ModalsModule { }
