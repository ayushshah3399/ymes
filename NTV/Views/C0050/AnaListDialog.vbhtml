@ModelType IEnumerable(Of NTV_SHIFT.M0010)

<style>
	.modal-body dt, dd {
		padding: 3px;
	}

	.table-scroll {
		width: 240px;
	}

	table.table-scroll tbody,
	table.table-scroll thead {
		display: block;
	}

	table.table-scroll tbody {
		height: 500px;
		width: 240px;
		overflow-y: auto;
		overflow-x: hidden;
	}
</style>

<div Class="modal fade" id="myModalAna" tabindex="-1" role="dialog">
	<div Class="modal-dialog modal-sm" role="document">
		<div Class="modal-content">
			<div Class="modal-header">
				<Button type="button" Class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></Button>
				<h4 Class="modal-title">アナウンサー選択</h4>
			</div>
			<div Class="modal-body">
				@If Model IsNot Nothing Then
					@<table id="tblAna" class="table table-hover table-bordered table-scroll">

						@For Each item In Model
							@<tr>
								<td style="width:40px;">
									<input type="checkbox" />
								</td>
								<td style="width:200px;">
									@item.USERNM
								</td>
								<td style="visibility:hidden;">
									@item.USERID
								</td>
							</tr>
						Next
					</table>
				End If
			</div>
			<div Class="modal-footer">
				<Button id="modal-selectana" type="button" Class="btn btn-default" data-dismiss="modal" style="float: left;">選択</Button>
				<Button id="modal-close" type="button" Class="btn btn-primary" data-dismiss="modal">閉じる</Button>
			</div>
		</div>
	</div>
</div>
