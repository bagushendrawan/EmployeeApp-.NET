const toastTrigger = document.getElementById("liveToastBtn");
const toastLiveExample = document.getElementById("liveToast");
const myModal = document.getElementById("myModal");
const myInput = document.getElementById("myInput");
const baseUrl = `${window.location.protocol}//${window.location.host}`;

function showToast(text) {
  document.getElementById("toast-text").innerHTML = text;
  const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
  toastBootstrap.show();
}

function fetchData(fetchedData) {
  let html = "";
  fetchedData.forEach((item, index) => {
    html += `<tr>
          <th scope="row">${index}</th>
          <td>${item.nik}</td>
          <td>${item.name}</td>
          <td>${calculateAge(item.birth)}</td>
          <td>${item.birth}</td>
          <td>${item.gender}</td>
          <td>${item.address}</td>
          <td>${item.country}</td>
          <td class="d-flex gap-2">
            <button
              type="button"
              class="btn btn-secondary btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#detailModal"
              data-nik=${item.nik}
            >
              Detail</button
            ><button
              type="button"
              class="btn btn-primary btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#editModal"
              data-nik=${item.nik}
            >
              Edit</button
            ><button
              type="button"
              class="btn btn-danger btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#deleteModal"
              data-nik=${item.nik}
              data-nama=${item.name}
            >
              Delete
            </button>
          </td>
        </tr>`;
  });
  html += "";
  $("#data-container").html(html);

  getCountries(function () {
    $("#countries-data-add").val(data.country);
  }, "countries-data-add");
}

$(document).ready(function () {
  $.ajax({
    url: `${baseUrl}/employees`,
    method: "GET",
    success: function (data) {
      let html = "";
      data.forEach((item, index) => {
        html += `<tr>
          <th scope="row">${index}</th>
          <td>${item.nik}</td>
          <td>${item.name}</td>
          <td>${calculateAge(item.birth)}</td>
          <td>${item.birth}</td>
          <td>${item.gender}</td>
          <td>${item.address}</td>
          <td>${item.country}</td>
          <td class="d-flex gap-2">
            <button
              type="button"
              class="btn btn-secondary btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#detailModal"
              data-nik=${item.nik}
            >
              Detail</button
            ><button
              type="button"
              class="btn btn-primary btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#editModal"
              data-nik=${item.nik}
            >
              Edit</button
            ><button
              type="button"
              class="btn btn-danger btn-sm"
              data-bs-toggle="modal"
              data-bs-target="#deleteModal"
              data-nik=${item.nik}
              data-nama=${item.name}
            >
              Delete
            </button>
          </td>
        </tr>`;
      });
      html += "";
      $("#data-container").html(html);

      getCountries(function () {
        $("#countries-data-add").val(data.country);
      }, "countries-data-add");
    },
    error: function (xhr, status, error) {
      $("#data-container").html("<p>Error: " + error + "</p>");
    },
  });
});

document.addEventListener("DOMContentLoaded", function () {
  let deleteModal = document.getElementById("deleteModal");
  let deleteButton = document.getElementById("delete-data");

  deleteModal.addEventListener("shown.bs.modal", function (event) {
    let triggerButton = event.relatedTarget;

    let name = triggerButton.getAttribute("data-nama");
    let nik = triggerButton.getAttribute("data-nik");

    document.getElementById("deleted-name").textContent = name;
    deleteButton.setAttribute("data-nik", nik);
  });
});

$("#editModal").on("show.bs.modal", function (event) {
  let button = $(event.relatedTarget);
  let nik = button.data("nik");

  $.ajax({
    url: `${baseUrl}/employees/${nik}`,
    method: "GET",
    success: function (data) {
      document.getElementById("name-data-edit").value = data.name;
      document.getElementById("nik-data-edit").value = data.nik;
      if (data.gender == "Male")
        document.getElementById("gender1-data-edit").checked = true;
      else document.getElementById("gender2-data-edit").checked = true;

      document.getElementById("birth-data-edit").value = data.birth;
      document.getElementById("address-data-edit").value = data.address;
      document.getElementById("countries-data-edit").value = data.country;
      getCountries(function () {
        $("#countries-data-edit").val(data.country);
      }, "countries-data-edit");
    },
    error: function (xhr, status, error) {
      $("#modal-edit").html("<p>Error: " + error + "</p>");
    },
  });
});

$("#detailModal").on("show.bs.modal", function (event) {
  let button = $(event.relatedTarget);
  let nik = button.data("nik");

  $.ajax({
    url: `${baseUrl}/employees/${nik}`,
    method: "GET",
    success: function (data) {
      document.getElementById("nama-data").value = data.name;
      document.getElementById("nik-data").value = data.nik;
      if (data.gender == "Male")
        document.getElementById("gender1-data").checked = true;
      else document.getElementById("gender2-data").checked = true;

      document.getElementById("birth-data").value = data.birth;
      document.getElementById("address-data").value = data.address;
      document.getElementById("countries-data").value = data.country;
      getCountries(function () {
        $("#countries-data").val(data.country);
      }, "countries-data");
    },
    error: function (xhr, status, error) {
      $("#modal-detail").html("<p>Error: " + error + "</p>");
    },
  });
});

function calculateAge(birthDate) {
  const today = new Date();
  const birth = new Date(birthDate);
  let age = today.getFullYear() - birth.getFullYear();
  const monthDiff = today.getMonth() - birth.getMonth();

  if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
    age--;
  }

  return age;
}

function getCountries(callback, countriesSelectID) {
  $.ajax({
    url: `${baseUrl}/countries.json`,
    method: "GET",
    success: function (data) {
      console.log("countries", data);
      let html = "";
      data.forEach((item, index) => {
        html += `<option value=${item.name}>${item.name}</option>`;
      });
      html += "";
      $(`#${countriesSelectID}`).html(html);
      if (typeof callback === "function") {
        callback();
      }
    },
    error: function (xhr, status, error) {
      console.log(error);
      $(`#${countriesSelectID}`).html("<p>Error: " + error + "</p>");
    },
  });
}

function addData(event) {
  event.preventDefault();
  const data = {
    nik: $("#nik-data-add").val(),
    name: $("#name-data-add").val(),
    gender: $("input[name='Gender']:checked").val(),
    birth: $("#birth-data-add").val(),
    address: $("#address-data-add").val(),
    country: $("#countries-data-add").val(),
  };

  $.ajax({
    url: `${baseUrl}/employees`,
    method: "POST",
    contentType: "application/json",
    data: JSON.stringify(data),
    success: function (response) {
      showToast(`Successfully Add Data, Name: ${data.name}`);
      window.location.reload();
    },
    error: function (error) {
      showToast(
        "Failed to Add Data, Please check id NIK is already registered or the Birthdate is invalid"
      );
    },
  });
}

function editData() {
  const data = {
    nik: $("#nik-data-edit").val(),
    name: $("#name-data-edit").val(),
    gender: $("input[name='Gender-edit']:checked").val(),
    birth: $("#birth-data-edit").val(),
    address: $("#address-data-edit").val(),
    country: $("#countries-data-edit").val(),
  };

  $.ajax({
    url: `${baseUrl}/employees/${data.nik}`,
    method: "PUT",
    contentType: "application/json",
    data: JSON.stringify(data),
    success: function (response) {
      showToast(`Successfully Update Data, NIK: ${data.nik}`);
      setTimeout(() => {
        window.location.reload();
      }, 700);
    },
    error: function (error) {
      showToast("Failed to update data, please try again");
    },
  });
}

function deleteData() {
  let deleteButton = document.getElementById("delete-data");
  let nik = deleteButton.getAttribute("data-nik");
  $.ajax({
    url: `${baseUrl}/employees/${nik}`,
    method: "DELETE",
    success: function (response) {
      showToast(`Successfully Delete Data, NIK: ${nik}`);
      setTimeout(() => {
        window.location.reload();
      }, 700);
    },
    error: function (error) {
      showToast("Failed to delete data, Please try again");
    },
  });
}

function filterData(event) {
  event.preventDefault();
  let nik = $("#search-nik").val();
  let name = $("#search-name").val();

  $.ajax({
    url: `${baseUrl}/employees/find?nik=${nik}&name=${name}`,
    method: "GET",
    success: function (response) {
      showToast(`Search Data Success!, NIK: ${nik} Name: ${name}`);
      fetchData(response);
    },
    error: function (error) {
      showToast("Search Data Failed, Please try again");
    },
  });
}
