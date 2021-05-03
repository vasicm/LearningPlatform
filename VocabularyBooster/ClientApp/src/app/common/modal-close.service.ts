import { Injectable } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";

@Injectable({
  providedIn: "root",
})
export class ModalCloseService {
  constructor(private modalService: BsModalService) {}

  hide() {
    // fix for missing scrollbars after closing modal
    // this will work only if nested dialogs are NOT used
    // https://github.com/valor-software/ngx-bootstrap/issues/2137
    console.log(document.body.classList);
    if (document.body.classList.contains("modal-open")) {
      document.body.classList.remove("modal-open");
    }

    this.modalService.hide(1);
  }
}
